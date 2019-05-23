namespace MathProblemCreator.BussinessLogik.Problems
{
    public interface IProblem
    {
        string ProblemName { get; }
        string ProblebDescription { get; }
        string ProblemParametrs { get; }

        string Generate();
    }
}
