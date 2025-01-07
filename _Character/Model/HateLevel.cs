using System.Collections;

public class HateLevel
{
    //적대 캐릭터 Id
    private int playerId;

    //적대 수치
    private int hateLevel;

    public void setId(int id) { playerId = id; }
    public int getId() { return playerId; }

    public void setHateLevel(int hateLevel) {

        if (hateLevel < 50) this.hateLevel = 50;
        else this.hateLevel = hateLevel;
    }

    public int getHateLevel() { return hateLevel; }
    
}
