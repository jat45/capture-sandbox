using System;
using System.Collections.Generic;
using System.Linq;

namespace CaptureSandbox.Model
{
  public sealed class GarmentModel
  {
    public GarmentModel(Guid id)
    {
      Id = id;
      PhotoSets = PhotoSetModel.NonSleeved().ToList();
    }

    public Guid Id { get; private set; }
    public List<PhotoSetModel> PhotoSets { get; private set; }
  }
}