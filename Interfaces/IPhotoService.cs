using CloudinaryDotNet.Actions;

namespace MvcMovie.Interfaces
{
	public interface IPhotoService
	{
		Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
		Task<DeletionResult> DeletePhotoAsync(string publicid);
	}
}