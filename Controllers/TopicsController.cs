using Microsoft.AspNetCore.Mvc;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        public TopicsController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult> GetTopics()
        {
            try
            {
                var topics = await _topicRepository.GetTopicsAsync();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{topicName}")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult> GetTopic(string topicName)
        {
            try
            {
                var topic = await _topicRepository.GetTopicByNameAsync(topicName);

                if (topic == null)
                {
                    return NotFound();
                }

                return Ok(topic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<ActionResult> CreateTopic([FromBody] Topics topic)
        {
            try
            {
                await _topicRepository.CreateTopicAsync(topic);

                return CreatedAtAction(nameof(GetTopic), new { topicName = topic.Topic_Name }, topic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
