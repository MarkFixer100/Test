﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum OrderStatus:byte
    {
        Pending,
        Processing, 
        Sended,    
        Delivered,  
        Cancelled
    }
}
