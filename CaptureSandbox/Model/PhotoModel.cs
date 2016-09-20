namespace CaptureSandbox.Model
{
  public sealed class PhotoModel
  {
    public PhotoModel(string name, string path)
    {
      Path = path;
      Name = name;
    }

    public string Name { get; private set; }
    public string Path { get; private set; }
    public string ImagePath { get; set; }
  }
}