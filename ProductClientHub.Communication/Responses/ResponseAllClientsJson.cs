namespace ProductClientHub.Communication.Responses;
public class ResponseAllClientsJson
{
    public ResponsePaginationJson Pagination { get; set; } = default!;
    public List<ResponseShortClientJson> Clients { get; set; } = [];
}

