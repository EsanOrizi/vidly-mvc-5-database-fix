﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        // customer Id
        public int CustomerId { get; set; }
        // List of movies
        public List<int> MovieIds { get; set; }


    }
}