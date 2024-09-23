﻿using Dome.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface GenericRepositort<T> where T : ModelBase
    {

        // Create IGenericRopositort Because Don't Repert Lot Of Model 
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T item);
        int Update(T item);
        int Delete(T item);
        
    }
}

