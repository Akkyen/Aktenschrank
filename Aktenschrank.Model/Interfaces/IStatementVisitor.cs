using Aktenschrank.Model.Statements;
using Action = Aktenschrank.Model.Statements.Action;

namespace Aktenschrank.Model.Interfaces
{
    public interface IStatementVisitor
    {
        public void Visit(AStatement statement);

        public void Visit(AStatementWithBlock statementWithBlock);

        public void Visit(Action action);

        public void Visit(Condition condition);
        
    }
}
