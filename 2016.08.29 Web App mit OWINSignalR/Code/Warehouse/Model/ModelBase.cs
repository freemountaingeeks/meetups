using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Warehouse.Model
{
    public class ModelBase: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged event

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It is defined that the method to raise an event handler is called 'RaiseXxx'")]
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));                
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It is defined that the method to raise an event handler is called 'RaiseXxx'")]
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression != null)
            {
                if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpr = propertyExpression.Body as MemberExpression;
                    if (memberExpr != null)
                    {
                        string propertyName = memberExpr.Member.Name;
                        RaisePropertyChanged(propertyName);
                        return;
                    }
                }
            }
            throw new ArgumentException("invalid Property", "propertyExpression");
        }
                
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "It is defined that the method to raise an event handler is called 'RaiseXxx'")]
        protected void RaisePropertyChangedWithoutLog(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }        
        
        #endregion
        
    }
}
