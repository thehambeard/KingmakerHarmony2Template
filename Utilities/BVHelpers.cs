using Kingmaker;
using Kingmaker.Blueprints;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BetterVendors.Utilities
{
    class BVHelpers
    {
        static LibraryScriptableObject Library => Main.Library;
        public static Vector3 GetPlayerPosistion()
        {
            return Game.Instance.Player.MainCharacter.Value.Position;
        }

        public static Quaternion GetPlayerRotation()
        {
            return Quaternion.LookRotation(Game.Instance.Player.MainCharacter.Value.OrientationDirection);
        }
        public static object DeepClone(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }
    }
}
