using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVMTraining
{
    class Calc:BindableBase
    {
        private double lhs;
        public double Lhs
        {
            get { return lhs; }
            set { SetProperty(ref lhs, value); }
        }

        private double rhs;
        public double Rhs
        {
            get { return rhs; }
            set { SetProperty(ref rhs, value); }
        }

        private OperatorType operatorType;

        public OperatorType OType
        {
            get { return this.operatorType; }
            set { this.SetProperty(ref this.operatorType, value); }
        }

        private double answer;
        private AppContext appContext;

        public Calc(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public double Answer
        {
            get { return answer; }
            set { SetProperty(ref answer, value); }
        }

        public enum OperatorType
        {
            Add,
            Sub,
            Mul,
            Div,
        }
        public void Execute()
        {
            switch (this.OType)
            {
                case OperatorType.Add:
                    this.Answer = this.Lhs + this.Rhs;
                    break;
                case OperatorType.Sub:
                    this.Answer = this.Lhs - this.Rhs;
                    break;
                case OperatorType.Mul:
                    this.Answer = this.Lhs * this.Rhs;
                    break;
                case OperatorType.Div:
                    if (this.Rhs == 0)
                    {
                        this.appContext.Message = "0除算エラー";
                        return;
                    }
                    this.Answer = this.Lhs / this.Rhs;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

    }
}
