﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EbayView.Models.ViewModel.Offers
{
    public class CreateOffersInputModel
    {
        public int OfferId { get; set; }
        public int ProductId { get; set; }
        public int AdminId { get; set; }
        public float OldPrice { get; set; }
        public float NewPrice { get; set; }
        public int Precentage { get; set; }
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}
