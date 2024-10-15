
public class Cell
{
    public float id;
    public Item ItemData;
    public int countItem;
    public Cell()
    {

    }
    public Cell(Cell cell)
    {
        this.ItemData = cell.ItemData;
        this.countItem = cell.countItem;
        this.id = cell.id;
    }
    public Cell(Item itemData, int countItem)
    {
        this.ItemData= itemData;
        this.countItem = countItem;
        id = itemData.id;
    }

}
