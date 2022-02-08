using System;
using BeatTim.Models;
using BeatTim.Services.DTO.OutputDTO;

namespace BeatTim.Services.OutputDTO
{
    public class PersonalBeatsDto: BeatDto
    {
        public PersonalBeatsDto(Beat beat): base(beat)
        {
            GenreName = beat.Genre?.GenreName;
            VerificationStatus = beat.VerificationStatus;
        }
        
        public string GenreName { get; set; }
        public VerificationStatus VerificationStatus  { get; set; }
    }
}