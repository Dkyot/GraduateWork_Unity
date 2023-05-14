public class GraphEdge
{
    public RoomData connectedRoom;
    public int edgeWeight;

    public GraphEdge(RoomData connectedRoom, int weight) {
        this.connectedRoom = connectedRoom;
        edgeWeight = weight;
    }
}