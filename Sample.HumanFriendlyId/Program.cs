using Sample.HumanFriendlyId;

var humanFriendlyId = new HumanFriendlyId(16);
Console.WriteLine($"Id: {humanFriendlyId.Id}");
Console.WriteLine($"Display as: {humanFriendlyId.DisplayId}");