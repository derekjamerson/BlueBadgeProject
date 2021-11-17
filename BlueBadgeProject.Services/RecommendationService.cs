﻿using BlueBadgeProject.Data;
using ElevenNote.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Services
{
    public class RecommendationService
    {
        public bool CreateRecommendation(RecommendationCreate model)
        {
            var entity = new Recommendation()
            {
                SongId = model.SongId,
                UserProfileId = model.UserProfileId,
                GroupId = model.GroupId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Recommendations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}