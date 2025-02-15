﻿using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Response.Withdrawal
{
    public class CreateWithdrawalResponse
    {
        public Guid Id { get; set; }
        public long Amount { get; set; }
        public WithdrawalStatus WithdrawalStatus { get; set; }
        public string? Note { get; set; }
    }
}
