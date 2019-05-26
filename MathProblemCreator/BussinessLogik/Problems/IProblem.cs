namespace MathProblemCreator.BussinessLogik.Problems
{
    public interface IProblem
    {
        (string Problem, string Answer) Generate();
    }
}
