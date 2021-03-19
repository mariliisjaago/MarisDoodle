using DoodleWebMvc.Models;
using DoodleWebMvc.Utils.Contracts;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoodleWebMvc.Utils
{
    public class ModelPopulator : IModelPopulator
    {
        private readonly IPollRoutine _pollRoutine;

        public ModelPopulator(IPollRoutine pollRoutine)
        {
            _pollRoutine = pollRoutine;
        }

        public async Task<PollFlexibleModel> PopulatePollAndOptionsForDisplay(int pollId)
        {
            PollFlexibleModel model = new PollFlexibleModel();
            model.Poll = await _pollRoutine.GetBasicPoll(pollId);
            model.Options = await _pollRoutine.GetPollOptionsForDisplay(pollId);

            return model;
        }

        public async Task<PollVotingModel> PopulatePollAndOptionsForVoting(int pollId)
        {
            PollVotingModel model = new PollVotingModel();
            model.Poll = await _pollRoutine.GetBasicPoll(pollId);
            model.Options = await _pollRoutine.GetPollOptionsForVoting(pollId);

            return model;
        }

        public async Task<PollFlexibleModel> PopulatePollName(int id)
        {
            PollFlexibleModel model = new PollFlexibleModel();

            model.Poll = await _pollRoutine.GetBasicPoll(id);

            return model;
        }

        public List<VoteModel> TransformRawOptionDataToVotes(string voterName, List<PollOptionVotingModel> options)
        {
            List<VoteModel> output = new List<VoteModel>();

            foreach (var option in options)
            {
                if (option.VotedFor == true)
                {
                    VoteModel vote = new VoteModel
                    {
                        VoterName = voterName,
                        OptionId = option.Id,
                        CreatedOn = option.VotedOn
                    };

                    output.Add(vote);
                }
            }

            return output;
        }
    }
}
