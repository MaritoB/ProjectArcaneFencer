
public interface ITriggerCheckable
{
    bool IsWithinAggroDistance { get; set; }
    bool IsWithinStrikingDistance { get; set; }
    void SetAggroStatus(bool isAggroed);
    void SetStrikingDistanceBool(bool isStrikingDistance);

}
