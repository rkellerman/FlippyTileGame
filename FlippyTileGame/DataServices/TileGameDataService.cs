using FlippyTileGame.DataServiceInterfaces;
using FlippyTileGame.Model;
using FlippyTileGame.Settings;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlippyTileGame.DataServices
{
    public class TileGameDataService : ITileGameDataService
    {
        public TileGameDataService()
        {

        }
        public List<FlippyTileModel> GeTileModels()
        {

            // Get Images in folder
            var images = Directory.EnumerateFiles(FlippyTileGameSettings.GameDataFolder, "*.png").ToList();


            // Loop through Images and create Models
            // Add Models to List

            var FlippyTileModelList = new List<FlippyTileModel>();

            for (var i = 0; i < images.Count(); i++)
            {
                var model = new FlippyTileModel
                {
                    ImagePath = images[i],
                    PairId = i
                };

                FlippyTileModelList.Add(model);

            }

            // Return List
            return FlippyTileModelList;


        }
    }
}
