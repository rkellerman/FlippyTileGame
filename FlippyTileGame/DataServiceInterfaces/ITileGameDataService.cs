using FlippyTileGame.Model;
using System.Collections.Generic;

namespace FlippyTileGame.DataServiceInterfaces
{
    public interface ITileGameDataService
    {
        List<FlippyTileModel> GeTileModels();
    }
}
