namespace phish.prediction.api.Features.Cloudflare.Models;

using System;
using System.Collections.Generic;

public class ScanResult
{
    public string message { get; set; }
    public Data data { get; set; }
    public Lists lists { get; set; }
    public Page page { get; set; }
    public Scanner scanner { get; set; }
    public Stats stats { get; set; }
    public Task task { get; set; }
    public Verdicts verdicts { get; set; }
}

public class Data
{
    public List<Console> console { get; set; }
    public List<Cookie> cookies { get; set; }
    public List<Global> globals { get; set; }
    public List<Link> links { get; set; }
    public List<Performance> performance { get; set; }
    public List<RequestWrapper> requests { get; set; }
}

public class Console
{
    public Message message { get; set; }
}

public class Message
{
    public string level { get; set; }
    public string source { get; set; }
    public string text { get; set; }
    public string url { get; set; }
}

public class Cookie
{
    public string domain { get; set; }
    public long expires { get; set; }
    public bool httpOnly { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public string priority { get; set; }
    public bool sameParty { get; set; }
    public bool secure { get; set; }
    public bool session { get; set; }
    public int size { get; set; }
    public int sourcePort { get; set; }
    public string sourceScheme { get; set; }
    public string value { get; set; }
}

public class Global
{
    public string prop { get; set; }
    public string type { get; set; }
}

public class Link
{
    public string href { get; set; }
    public string text { get; set; }
}

public class Performance
{
    public double duration { get; set; }
    public string entryType { get; set; }
    public string name { get; set; }
    public double startTime { get; set; }
}

public class RequestWrapper
{
    public RequestDetails request { get; set; }
    public ResponseDetails response { get; set; }
    public List<Request> requests { get; set; }
}

public class RequestDetails
{
    public string documentURL { get; set; }
    public bool hasUserGesture { get; set; }
    public Initiator initiator { get; set; }
    public bool redirectHasExtraInfo { get; set; }
    public Request request { get; set; }
    public string requestId { get; set; }
    public string type { get; set; }
    public long wallTime { get; set; }
    public string frameId { get; set; }
    public string loaderId { get; set; }
    public bool primaryRequest { get; set; }
    public RedirectResponse redirectResponse { get; set; }
}

public class Initiator
{
    public string host { get; set; }
    public string type { get; set; }
    public string url { get; set; }
}

public class Request
{
    public string initialPriority { get; set; }
    public bool isSameSite { get; set; }
    public string method { get; set; }
    public string mixedContentType { get; set; }
    public string referrerPolicy { get; set; }
    public string url { get; set; }
    public Dictionary<string, string> headers { get; set; }
}

public class RedirectResponse
{
    public string charset { get; set; }
    public string mimeType { get; set; }
    public string protocol { get; set; }
    public string remoteIPAddress { get; set; }
    public int remotePort { get; set; }
    public List<SecurityHeader> securityHeaders { get; set; }
    public string securityState { get; set; }
    public int status { get; set; }
    public string statusText { get; set; }
    public string url { get; set; }
    public Dictionary<string, string> headers { get; set; }
}

public class SecurityHeader
{
    public string name { get; set; }
    public string value { get; set; }
}

public class ResponseDetails
{
    public Asn asn { get; set; }
    public int dataLength { get; set; }
    public int encodedDataLength { get; set; }
    //public Geoip geoip { get; set; }
    public bool hasExtraInfo { get; set; }
    public string requestId { get; set; }
    public Response response { get; set; }
    public int size { get; set; }
    public string type { get; set; }
    public bool contentAvailable { get; set; }
    public string hash { get; set; }
}

public class Asn
{
    public string asn { get; set; }
    public string country { get; set; }
    public string description { get; set; }
    public string ip { get; set; }
    public string name { get; set; }
    public string org { get; set; }
}

// public class Geoip
// {
//     public string city { get; set; }
//     public string country { get; set; }
//     public string country_name { get; set; }
//     public string region { get; set; }
// }

public class Response
{
    public string charset { get; set; }
    public string mimeType { get; set; }
    public string protocol { get; set; }
    public string remoteIPAddress { get; set; }
    public int remotePort { get; set; }
    public SecurityDetails securityDetails { get; set; }
    public List<SecurityHeader> securityHeaders { get; set; }
    public string securityState { get; set; }
    public int status { get; set; }
    public string statusText { get; set; }
    public string url { get; set; }
    public Dictionary<string, string> headers { get; set; }
}

public class SecurityDetails
{
    public int certificateId { get; set; }
    public string certificateTransparencyCompliance { get; set; }
    public string cipher { get; set; }
    public bool encryptedClientHello { get; set; }
    public string issuer { get; set; }
    public string keyExchange { get; set; }
    public string keyExchangeGroup { get; set; }
    public string protocol { get; set; }
    public List<string> sanList { get; set; }
    public int serverSignatureAlgorithm { get; set; }
    public string subjectName { get; set; }
    public int validFrom { get; set; }
    public int validTo { get; set; }
}

public class Lists
{
    public List<string> asns { get; set; }
    public List<Certificate> certificates { get; set; }
    public List<string> continents { get; set; }
    public List<string> countries { get; set; }
    public List<string> domains { get; set; }
    public List<string> hashes { get; set; }
    public List<string> ips { get; set; }
    public List<string> linkDomains { get; set; }
    public List<string> servers { get; set; }
    public List<string> urls { get; set; }
}

public class Certificate
{
    public string issuer { get; set; }
    public string subjectName { get; set; }
    public int validFrom { get; set; }
    public int validTo { get; set; }
}









public class Page
{
    public string apexDomain { get; set; }
    public string asn { get; set; }
    public string asnname { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string domain { get; set; }
    public string ip { get; set; }
    public string mimeType { get; set; }
    public string server { get; set; }
    public string status { get; set; }
    public string title { get; set; }
    public int tlsAgeDays { get; set; }
    public string tlsIssuer { get; set; }
    public int tlsValidDays { get; set; }
    public string tlsValidFrom { get; set; }
    public string url { get; set; }
}

public class Scanner
{
    public string colo { get; set; }
    public string country { get; set; }
}

public class Stats
{
    public List<DomainStats> domainStats { get; set; }
    public List<IpStats> ipStats { get; set; }
}

public class DomainStats
{
    public int count { get; set; }
    public List<string> countries { get; set; }
}

public class IpStats
{
    public Asn asn { get; set; }
    public List<string> countries { get; set; }
}

public class Task
{
    public string apexDomain { get; set; }
    public string domain { get; set; }
}

public class Verdicts
{
    public Overall overall { get; set; }
}

public class Overall
{
    public bool malicious { get; set; }
}
