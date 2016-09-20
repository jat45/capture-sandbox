using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaptureSandbox.Model
{
  public interface IDatabase
  {
    void CreateGarment(Guid id);
    void AddSleeves();
    void RemoveSleeves();
    IEnumerable<PhotoSetModel> GetPhotos();
    Task Publish();
  }
}