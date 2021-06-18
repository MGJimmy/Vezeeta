﻿using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class OfferRepository:BaseRepository<Offer>
    {
        public OfferRepository(DbContext context):base(context)
        {

        }

        public IEnumerable<Offer> GetAllWithSubOffer()
        {
            return DbSet.Include(i => i.SubOffers);
        }



    }
}
