using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVMTraining
{
    class AppContext:BindableBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public Calc Calc { get; private set; }

        public AppContext()
        {
            this.Calc = new Calc(this);
        }
    }
}
