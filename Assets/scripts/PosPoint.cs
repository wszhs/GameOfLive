
public class PosPoint  {

    public int x;
    public int y;
    public PosPoint() {
        x = -1;
        y = -1;
    }
    public PosPoint(int ax, int ay) {
        x = ax;
        y = ay;
    }
    public static PosPoint operator +(PosPoint a, PosPoint b) {
        PosPoint p=new PosPoint();
        p.x = a.x + b.x;
        p.y = a.y + b.y;
        return p;

    }

   public  static PosPoint operator -(PosPoint a, PosPoint b) {
        PosPoint p = new PosPoint();
        p.x = a.x - b.x;
        p.y = a.y - b.y;
        return p;
    }
    public  static  bool operator ==(PosPoint a, PosPoint b) {

        if (object.Equals(a, null) && object.Equals(b, null))
            return true;

        else if (object.Equals(a, null) || object.Equals(b, null))
            return false;

        return (a.x == b.x && a.y == b.y);
        

    }
    public static bool operator !=(PosPoint a, PosPoint b) {


        if (object.Equals(a, null) && object.Equals(b, null))
            return false;

        else if (object.Equals(a, null) || object.Equals(b, null))
            return true;
        return (a.x != b.x || a.y != b.y);
 

    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(PosPoint))
            return false;

        PosPoint id = obj as PosPoint;
        if (object.Equals(id, null))
            return false;

        return (id.x == this.x&&id.y==this.y);
    }

}
