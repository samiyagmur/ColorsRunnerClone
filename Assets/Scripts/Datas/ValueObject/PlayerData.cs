using System;

namespace Datas.ValueObject{

[Serializable]
public class PlayerData
{
    public PlayerMovementData MovementData;
}

[Serializable]
public class PlayerMovementData
{
    public float ForwardSpeed = 5f;
    public float SidewaysSpeed = 2f;
}
}