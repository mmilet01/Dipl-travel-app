using Core.Enums;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class FriendsRepository : GenericRepository<UsersRelationship>, IFriendsRepository
    {
        public FriendsRepository(mmiletaContext _context) : base(_context)
        {

        }
        public void FriendRequestSent(string fromEmail, int toId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == fromEmail);
            if (!_context.UsersRelationship.Any(x => x.FirstUserId == user.UserId))
            {
                var userRelationship = new UsersRelationship
                {
                    FirstUserId = user.UserId,
                    SecondUserId = toId,
                    RelationshipStatus = (int)UserRelationshipStatusEnum.FriendRequestSent,
                };
                _context.UsersRelationship.Add(userRelationship);

                var userRelationshipForSecondUser = new UsersRelationship
                {
                    FirstUserId = toId,
                    SecondUserId = user.UserId,
                    RelationshipStatus = (int)UserRelationshipStatusEnum.FriendRequestRecieved,
                };
                _context.UsersRelationship.Add(userRelationshipForSecondUser);
            }
        }

        public void FriendRequestResponded(string fromEmail, int toId, FriendRequestResponse response)
        {
            var user = _context.Users.Where(x => x.Email == fromEmail).Include(x => x.UsersRelationshipFirstUser).Include(x => x.UsersRelationshipSecondUser).SingleOrDefault();

            var relationshipOne = user.UsersRelationshipFirstUser.Where(x => (x.FirstUserId == user.UserId && x.SecondUserId == toId)).SingleOrDefault();
            var relationshipTwo = user.UsersRelationshipSecondUser.Where(x => (x.SecondUserId == user.UserId && x.FirstUserId == toId)).SingleOrDefault();

            if (response == FriendRequestResponse.Accepted)
            {
                relationshipOne.RelationshipStatus = (int)UserRelationshipStatusEnum.Friends;
                relationshipTwo.RelationshipStatus = (int)UserRelationshipStatusEnum.Friends;

            }
            else if (response == FriendRequestResponse.Declined)
            {
                relationshipOne.RelationshipStatus = (int)UserRelationshipStatusEnum.MaybeDenied;
                relationshipTwo.RelationshipStatus = (int)UserRelationshipStatusEnum.MaybeDenied;
            }
        }

        public UsersRelationship GetRelationshipStatus(int firstUserId, int id)
        {
            var status = _context.UsersRelationship.Where(x => x.FirstUserId == firstUserId && x.SecondUserId == id).Include(x => x.RelationshipStatusNavigation).SingleOrDefault();
            return status;
        }
    }
}
