using DataBaseProvider.Models;
using Manager;
using Parsers.Model;
using Parsers.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataSyncService
{
    public interface ISyncDataService
    {
        bool Locked { get; set; }
        void Sync();
    }

    public class SyncDataService : ISyncDataService
    {
        private ICategoryManager _categoryManager;
        private IProviderManager _providerManager;
        private IContentManager _contentManager;
        private IParserService _parserService;

        public SyncDataService(ICategoryManager categoryManager, IProviderManager providerManager,
            IContentManager contentManager, IParserService parserService)
        {
            _categoryManager = categoryManager;
            _providerManager = providerManager;
            _contentManager = contentManager;
            _parserService = parserService;
            Locked = false;
        }

        public void Sync()
        {
            Locked = true;
            var categories = _categoryManager.ToList();
            categories.ForEach(c =>
            {
                var now = DateTime.Now;

                if (TimeToUpdate(c.LastSync, c.SyncPeriod))
                {
                    var provider = _providerManager.ToFilteredList(c);
                    provider.ForEach(p =>
                    {
                        var source = new XMLSource()
                        {
                            XmlReader = XmlReader.Create(p.ProviderURI)
                        };
                        var data = _parserService.Process(source);
                        var contentFromProvider = _contentManager.ToFilteredList(p).OrderBy(x => x.PublicationDate).ToList();
                        DateTimeOffset lastItemPubDate;
                        if (contentFromProvider.Count > 0)
                            lastItemPubDate = contentFromProvider.Last().PublicationDate;
                        else
                            lastItemPubDate = DateTimeOffset.MinValue;
                        data.Items.ToList().ForEach(x =>
                        {
                            if (x.PubDate > lastItemPubDate)
                            {
                                var tmpCtn = new ProviderContent
                                {
                                    ProviderId = p.Id,
                                    Title = x.Title,
                                    Content = x.Description,
                                    PublicationDate = new DateTime(x.PubDate.Year, x.PubDate.Month, x.PubDate.Day, x.PubDate.Hour, x.PubDate.Minute, x.PubDate.Second),
                                    Author = ""
                                };
                                _contentManager.Add(tmpCtn);
                            }
                        });
                        p.LastSync = now;
                        _providerManager.Update(p);
                    });
                    c.LastSync = now;
                    _categoryManager.Update(c);
                }
            });
            Locked = false;
        }

        private bool TimeToUpdate(DateTime lastUpdate, TimeSpan syncPeriod)
        {
            var now = DateTime.Now;
            var timeToSync = lastUpdate.Add(syncPeriod);
            var compo = DateTime.Compare(now, timeToSync);
            if (compo >= 0) return true;
            return false;
        }

        private bool TimeToUpdate(long lastUpdate, long syncPeriod)
        {
            var now = DateTime.Now.ToBinary();
            var fromLast = lastUpdate + syncPeriod;
            if (now > fromLast) return true;
            return false;
        }

        public bool Locked { get; set; }
    }
}
