﻿using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public interface IRegisterManagmentServiceClient
    {
        Organisation CreateOrganisation(Organisation organisation);
        Organisation UpdateOrganisation(Organisation organisation);
        Service CreateService(Service service);
        Service UpdateService(Service service);
    }
}
