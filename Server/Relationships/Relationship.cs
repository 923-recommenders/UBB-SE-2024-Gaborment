namespace UBB_SE_2024_Gaborment.Server.Relationships
{
    internal interface Relationship
    {
        //nu am vorbit de setteri dar poate merge in caz ca e vreo implementare de schimbare de username din partea echipelor de conturi
        string getSender();
        string getReceiver();
    }
}
