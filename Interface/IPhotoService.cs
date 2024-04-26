namespace ReadEase_C_.Interface
{
    public interface IPhotoService
    {
        byte[] GetPhoto(int UserId);
        void InsertPhoto(int UserId, byte[] photo);
    }
}