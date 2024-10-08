﻿using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Features.Topics.DTOs;
using MediatR;

namespace InspireMind.Education.Application.Features.Topics.Requests.Queries;
public sealed class GetSingleTopicQuery(Guid id) : IRequest<Result<TopicDto>>
{
    public Guid Id { get; } = id;
}
