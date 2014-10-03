using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DownloadingService
{   
    interface IDownloadingService
	{
        void Start();
        void Stop();
	}
    
    class DownloadingService : IDownloadingService
    {

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x => { 
                
            });
        }
    }
}
