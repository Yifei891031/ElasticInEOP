using System;


public class FeedPackage
{
        public string source_machine_name { get; set; }
    	public long sequence_number { get; set; }
    	public DateTime date_time { get; set; }
    	public string client_ip { get; set; }
    	public string client_hostname { get; set; }
    	public string server_ip { get; set; }
    	public string server_hostname { get; set; }
    	public string source_context { get; set; }
    	public string connector_id { get; set; }
    	public string source { get; set; }
    	public string event_id { get; set; }
    	public long internal_message_id { get; set; }
    	public string message_id { get; set; }
    	public Guid network_message_id { get; set; }
    	public string recipient_address { get; set; }
    	public string recipient_status { get; set; }
    	public long total_bytes { get; set; }
    	public int recipient_count { get; set; }
    	public string related_recipient_address { get; set; }:
    	public string reference { get; set; }
    	public string message_subject { get; set; }
    	public string sender_address { get; set; }
    	public string return_path { get; set; }
    	public string message_info { get; set; }
    	public string directionality { get; set; }
    	public string tenant_id { get; set; }
    	public string original_client_ip { get; set; }
    	public string original_server_ip { get; set; }
    	public string custom_data { get; set; }
    	public string traffic_type { get; set; }
    	public string log_id { get; set; }
    	public string schema_id { get; set; }
}
