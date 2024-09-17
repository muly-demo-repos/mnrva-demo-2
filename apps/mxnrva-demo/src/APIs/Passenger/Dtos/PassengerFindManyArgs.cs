using Microsoft.AspNetCore.Mvc;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PassengerFindManyArgs : FindManyInput<Passenger, PassengerWhereInput> { }
