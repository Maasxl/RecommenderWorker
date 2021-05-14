using RecommendationWorker.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Repositories.Interfaces
{
    public interface ICampsiteRatingRepository
    {
        IEnumerable<CampsiteRatingData> GetAllCampsiteRatingData ();
    }
}
