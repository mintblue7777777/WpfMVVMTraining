using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVMTraining
{
    class MainWindowViewModel:BindableBase
    {
        private string input;
        public string Input
        {
            get { return this.input; }
            set
            {
                this.SetProperty(ref this.input, value);
                //入力値に変更があるたびにCanExecuteの状態がかわったことを通知する
                this.ConvertCommand.RaiseCanExecuteChanged();
            }
        }
        private string output;
        public string Output
        {
            get { return this.output; }
            set { this.SetProperty(ref this.output, value); }
        }

        public DelegateCommand ConvertCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.ConvertCommand = new DelegateCommand(
                this.ConvertExecute,
                this.CanConvertExecute);

            this.OTypes = OperatorTypeViewModel.OperatorTypes;

            this.ExecuteCommand = new DelegateCommand(this.Execute, this.CanExecute);

            //モデルの監視
            this.appContext.PropertyChanged += this.appContextPropertyChanged;
            this.appContext.Calc.PropertyChanged += CalcPropertyChanged;
        }

        private bool CanExecute()
        {
            double dummy;
            if (!double.TryParse(this.Lhs,out dummy))
            {
                return false;
            }
            if (!double.TryParse(this.Rhs, out dummy))
            {
                return false;
            }

            if (this.selectedOperatorType == null)
            {
                return false;
            }

            return true;

        }

        private void Execute()
        {
            this.appContext.Calc.Lhs = double.Parse(this.Lhs);
            this.appContext.Calc.Rhs = double.Parse(this.Rhs);
            this.appContext.Calc.OType = this.SelectedOperatorType.OType;
            this.appContext.Calc.Execute();


        }

        private void CalcPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Answer")
            {
                this.Answer = this.appContext.Calc.Answer;
            } 
        }

        private void appContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Message")
            {
                this.message = this.appContext.Message;
            }
        }

        private void ConvertExecute()
        {
            this.Output = this.Input.ToUpper();
        }

        private bool CanConvertExecute()
        {
            return !string.IsNullOrEmpty(this.input);
        }


        private AppContext appContext = new AppContext();

        private string lhs;
        public string Lhs
        {
            get { return lhs; }
            set { 
                SetProperty(ref lhs, value);
                this.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        private string rhs;
        public string Rhs
        {
            get { return rhs; }
            set { 
                SetProperty(ref rhs, value);
                this.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        private double answer;
        public double Answer
        {
            get { return answer; }
            set { SetProperty(ref answer, value); }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public OperatorTypeViewModel[] OTypes { get; private set; }
        private OperatorTypeViewModel selectedOperatorType;
        public OperatorTypeViewModel SelectedOperatorType
        {
            get { return selectedOperatorType; }
            set { 
                SetProperty(ref selectedOperatorType, value);
                this.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand ExecuteCommand { get; private set; }

        
    }
}
