using System;
using Nest;
using ElasticsearchInEOP;


public class FeedPackage : Package
{
    public void wrap(string[] strs)
    {
        this.source_machine_name = strs[0].Trim('"');
        if (!strs[1].Equals("\"\""))
        {
            this.sequence_number = long.Parse(strs[1].Trim('"'));
        }
        if (!strs[2].Equals("\"\""))
        {
            this.date_time = DateTime.Parse(strs[2].Trim('"'));
        }
        this.client_ip = strs[3].Trim('"');
        this.client_hostname = strs[4].Trim('"');
        this.server_ip = strs[5].Trim('"');
        this.server_hostname = strs[6].Trim('"');
        this.source_context = strs[7].Trim('"');
        this.connector_id = strs[8].Trim('"');
        this.source = strs[9].Trim('"');
        this.event_id = strs[10].Trim('"');
        if (!strs[11].Equals("\"\""))
        {

            this.internal_message_id = long.Parse(strs[11].Trim('"'));
        }


        this.message_id = strs[12].Trim('"');
        if (!strs[13].Equals("\"\""))
        {
            this.network_message_id = Guid.Parse(strs[13].Trim('"'));
        }
        this.recipient_address = strs[14].Trim('"');
        this.recipient_status = strs[15].Trim('"');
        if (!strs[16].Equals("\"\""))
        {
            this.total_bytes = long.Parse(strs[16].Trim('"'));
        }
        if (!strs[17].Equals("\"\""))
        {
            this.recipient_count = int.Parse(strs[17].Trim('"'));
        }
        this.related_recipient_address = strs[18].Trim('"');
        this.reference = strs[19].Trim('"');
        this.message_subject = strs[20].Trim('"');
        this.sender_address = strs[21].Trim('"');
        this.return_path = strs[22].Trim('"');
        this.message_info = strs[23].Trim('"');
        this.directionality = strs[24].Trim('"');
        this.tenant_id = strs[25].Trim('"');
        this.original_client_ip = strs[26].Trim('"');
        this.original_server_ip = strs[27].Trim('"');
        this.custom_data = strs[28].Trim('"');
        this.traffic_type = strs[29].Trim('"');
        this.log_id = strs[30].Trim('"');
        this.schema_id = strs[31].Trim('"');
    }
    

    public FeedPackage() { }

    [ElasticProperty(Index =FieldIndexOption.NotAnalyzed)]
    public string source_machine_name { get; set; }

    public long sequence_number { get; set; }
    public DateTime date_time { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string client_ip { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string client_hostname { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string server_ip { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string server_hostname { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string source_context { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string connector_id { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string source { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string event_id { get; set; }

    public long internal_message_id { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string message_id { get; set; }

    public Guid network_message_id { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string recipient_address { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string recipient_status { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public long total_bytes { get; set; }

    public int recipient_count { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string related_recipient_address { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string reference { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string message_subject { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string sender_address { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string return_path { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string message_info { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string directionality { get; set; }//24

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string tenant_id { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string original_client_ip { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string original_server_ip { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string custom_data { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string traffic_type { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string log_id { get; set; }

    [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
    public string schema_id { get; set; }
}
