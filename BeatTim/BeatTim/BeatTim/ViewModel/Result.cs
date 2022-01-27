namespace BeatTim.ViewModel
{
	public record Result<TValue>(bool IsSuccess, TValue Value);
}