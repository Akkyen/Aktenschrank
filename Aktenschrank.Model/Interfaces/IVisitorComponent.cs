namespace Aktenschrank.Model.Interfaces;

public interface IVisitorComponent
{
    public void Accept(IStatementVisitor statementVisitor);
}