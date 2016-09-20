using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CaptureSandbox.Model
{
  public class LocalDatabase : IDatabase
  {
    private GarmentModel garment;

    public void CreateGarment(Guid id)
    {
      garment = new GarmentModel(id);
    }

    public void AddSleeves()
    {
      garment.PhotoSets.AddRange(PhotoSetModel.Sleeves());
    }

    public void RemoveSleeves()
    {
      garment.PhotoSets.RemoveAll(e => e.IsSleeve);
    }

    public IEnumerable<PhotoSetModel> GetPhotos()
    {
      return garment.PhotoSets;
    }

    public async Task Publish()
    {
      var local = garment;
      var data = await Parse(local).ConfigureAwait(false);
      var path = Path.Combine(Path.GetTempPath(), "capture-database.txt");
      using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
      using (var writer = new StreamWriter(stream))
      {
        writer.WriteLine("### {0} ###", data.Id);
        foreach (var imageData in data.ImageDatas)
        {
          writer.WriteLine("Name: {0,-11} | Length: {1} bytes", imageData.Name, imageData.Size);
        }
      }
    }

    private async Task<GarmentData> Parse(GarmentModel garment)
    {
      var data = new GarmentData
      {
        Id = garment.Id,
        ImageDatas = await Parse(garment.PhotoSets.SelectMany(e => e.Photos)).ConfigureAwait(false)
      };
      return data;
    }

    private Task<IEnumerable<ImageData>> Parse(IEnumerable<PhotoModel> photos)
    {
      return
        Task.FromResult<IEnumerable<ImageData>>(
          photos.Distinct()
            .Select(e => new FileInfo(e.Path))
            .Select(e => new ImageData {Name = Path.GetFileNameWithoutExtension(e.FullName), Size = e.Length})
            .ToList());
    }

    private class GarmentData
    {
      public Guid Id { get; set; }
      public IEnumerable<ImageData> ImageDatas { get; set; }
    }

    private class ImageData
    {
      public string Name { get; set; }
      public long Size { get; set; }
    }
  }
}