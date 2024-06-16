using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace WebApp.Plugins;

public class OrderPlugin
{
    private static readonly List<string> Items = ["Pasta"];

    [KernelFunction, Description("Add an item to an order")]
    public static void AddItemToOrder([Description("Name of the item to add")] string itemName) => Items.Add(itemName);

    [KernelFunction, Description("Get items in the order")]
    public static List<string> GetItemsInOrder() => Items;
}