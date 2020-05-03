namespace Core.Shared.Response
{
    public class BasicInfoImage<T> : BasicInfo<T>,Interfaces.IHaveImage
    {       
        public string Image { get; set; }    
    }
}
