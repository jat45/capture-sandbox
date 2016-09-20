using System.Collections.Generic;
using System.IO;

namespace CaptureSandbox.Model
{
  public class PhotoSetModel
  {
    private PhotoSetModel(string name, bool isSleeve, IEnumerable<PhotoModel> photos)
    {
      Name = name;
      IsSleeve = isSleeve;
      Photos = photos;
    }

    public string Name { get; private set; }
    public bool IsSleeve { get; private set; }
    public IEnumerable<PhotoModel> Photos { get; private set; }

    public static IEnumerable<PhotoSetModel> NonSleeved()
    {
      var noArms = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "NoArms");
      return new[]
      {
        new PhotoSetModel("No Arms",
          false,
          new[]
          {
            new PhotoModel("Front", Path.Combine(noArms, "front.jpeg")),
            new PhotoModel("Front Right", Path.Combine(noArms, "front-right.jpeg")),
            new PhotoModel("Right", Path.Combine(noArms, "right.jpeg")),
            new PhotoModel("Back Right", Path.Combine(noArms, "back-right.jpeg")),
            new PhotoModel("Back", Path.Combine(noArms, "back.jpeg")),
            new PhotoModel("Back Left", Path.Combine(noArms, "back-left.jpeg")),
            new PhotoModel("Left", Path.Combine(noArms, "left.jpeg")),
            new PhotoModel("Front Left", Path.Combine(noArms, "front-left.jpeg"))
          })
      };
    }

    public static IEnumerable<PhotoSetModel> Sleeves()
    {
      var armsFront = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ArmsFront");
      var armsBack = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ArmsBack");
      return new[]
      {
        new PhotoSetModel("Arms Front",
          true,
          new[]
          {
            new PhotoModel("Front", Path.Combine(armsFront, "front.jpeg")),
            new PhotoModel("Front Right", Path.Combine(armsFront, "front-right.jpeg")),
            new PhotoModel("Front Left", Path.Combine(armsFront, "front-left.jpeg"))
          }),
        new PhotoSetModel("Arms Back",
          true,
          new[]
          {
            new PhotoModel("Front Right", Path.Combine(armsBack, "front-right.jpeg")),
            new PhotoModel("Right", Path.Combine(armsBack, "right.jpeg")),
            new PhotoModel("Back Right", Path.Combine(armsBack, "back-right.jpeg")),
            new PhotoModel("Back", Path.Combine(armsBack, "back.jpeg")),
            new PhotoModel("Back Left", Path.Combine(armsBack, "back-left.jpeg")),
            new PhotoModel("Left", Path.Combine(armsBack, "left.jpeg")),
            new PhotoModel("Front Left", Path.Combine(armsBack, "front-left.jpeg"))
          })
      };
    }
  }
}