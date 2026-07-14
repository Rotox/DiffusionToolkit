namespace Diffusion.Common.Query;

public class MultiValueFilter
{
    public NodeOperation Operation { get; set; }
    public NodeComparison Comparison { get; set; }
    public string Value { get; set; }
}
