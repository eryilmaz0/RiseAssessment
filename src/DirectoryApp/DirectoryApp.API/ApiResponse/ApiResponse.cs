namespace DirectoryApp.API.ApiResponse;

public class ApiResponse<TData>
{
    public bool IsSuccess { get; set; }
    public string ResultMessage { get; set; }
    public TData Data { get; set; }

    public ApiResponse(bool isSuccess)
    {
        this.IsSuccess = isSuccess;
    }


    public ApiResponse(bool isSuccess, string resultMessage):this(isSuccess)
    {
        this.ResultMessage = resultMessage;
    }


    public ApiResponse(bool isSuccess, string resultMessage, TData data):this(isSuccess, resultMessage)
    {
        this.Data = data;
    }
}