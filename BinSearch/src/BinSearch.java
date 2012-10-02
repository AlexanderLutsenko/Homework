import java.util.*;

public class BinSearch {
	public static void main(String[] args) {
		BinSearchTree Tree = new BinSearchTree();
		System.out.println("������� �������� ������: ");
		Scanner in = new Scanner(System.in);	
		String str = in.nextLine();
		StringTokenizer tokenizer = new StringTokenizer(str);
		try {
			while(tokenizer.hasMoreTokens()) {
				Tree.addElement(Integer.parseInt(tokenizer.nextToken()));
			}
		} catch(Exception e){
			System.out.println("������ �����");
			System.exit(0);
		}
		in.close();

		System.out.println(Tree.toLSF());
		System.out.print("����� ������ � �������: ");
		for(Integer i : Tree) {
			System.out.print(i+" ");
		}		
	}
}