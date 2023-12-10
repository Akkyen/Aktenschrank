using Aktenschrank.Model;
using Aktenschrank.Model.Interfaces;
using Aktenschrank.Model.Statements;
using Action = Aktenschrank.Model.Statements.Action;

namespace Aktenschrank.Sorting
{
    public class StatementVisitor : IStatementVisitor
    {
        public void Visit(AStatement statement)
        {
            throw new NotImplementedException();
        }

        public void Visit(AStatementWithBlock statementWithBlock)
        {
            throw new NotImplementedException();
        }

        public void Visit(Action action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Condition condition)
        {
            throw new NotImplementedException();
        }
    }
}
