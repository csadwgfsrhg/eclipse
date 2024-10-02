namespace Eclipse.Content.Classes;

public sealed class HarvestDamageClass : DamageClass
{
    public override bool UseStandardCritCalcs { get; } = true;

    public override StatInheritanceData GetModifierInheritance(DamageClass damageClass) {
        return damageClass == Generic ? StatInheritanceData.Full : StatInheritanceData.None;
    }
}

