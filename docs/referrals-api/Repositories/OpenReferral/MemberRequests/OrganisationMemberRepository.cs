﻿using MongoDB.Driver;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OpenReferrals.Repositories.OpenReferral
{
    public class OrganisationMemberRepository : IOrganisationMemberRepository
    {
        private IMongoRepository<OrganisationMember> _repo;

        public OrganisationMemberRepository(IMongoRepository<OrganisationMember> repo)
        {
            _repo = repo;
        }

        public IEnumerable<OrganisationMember> GetAllMembers(string orgId)
        {
            return _repo.FilterBy(x => x.OrgId == orgId && x.Status == OrganisationMembersStatus.JOINED);
        }


        public IEnumerable<OrganisationMember> GetAllMembers()
        {
            return _repo.FilterBy(x => x.Status == OrganisationMembersStatus.JOINED);
        }

        public IEnumerable<OrganisationMember> GetRequestsAboutUser(string userId)
        {
            return _repo.FilterBy(x => x.UserId == userId);
        }

        public async Task<OrganisationMember> Find(string orgId, string userId)
        {
            return await _repo.FindOneAsync(x => x.OrgId == orgId && x.UserId == userId);
        }

        public IEnumerable<OrganisationMember> GetAllPendingRequests(string orgId)
        {
            return _repo.FilterBy(x => x.OrgId == orgId && x.Status == OrganisationMembersStatus.REQUESTED);
        }


        public async Task InsertOne(OrganisationMember member)
        {
            await _repo.InsertOneAsync(member);
        }

        public async Task UpdateOne(OrganisationMember member)
        {
            await _repo.ReplaceOneAsync(member);
        }
    }
}
