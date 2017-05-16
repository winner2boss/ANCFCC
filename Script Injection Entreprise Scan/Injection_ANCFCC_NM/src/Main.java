import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.HashMap;
import java.util.Properties;
import java.util.StringTokenizer;
import java.util.logging.Level;

import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESKeySpec;
import org.apache.commons.io.FileUtils;


public class Main {

	public static String cheminErreur;
	public static String cheminArchiveSource;
	public static String cheminArchiveDestination;
	public static String destination;
	public static String JDBC_DRIVER_CLASS;
	public static String JDBC_CONNECTION_URL;
	public static Connection connection = null;
	public static DesEncrypter encrypter = null;
	public static String option_cryptage;

	public static void main(String[] args) throws FileNotFoundException,
			IOException, SQLException, ClassNotFoundException, InvalidKeyException, NoSuchAlgorithmException, InvalidKeySpecException {
		// TODO Auto-generated method stub
		Properties prop = load("config.properties");
		String cheminSource = prop.getProperty("chemin.source");
		String cheminCertificat = prop.getProperty("chemin.certificat");
		destination = prop.getProperty("chemin.destination");
		cheminArchiveSource = prop.getProperty("chemin.archive.source");
		cheminArchiveDestination = prop.getProperty("chemin.archive.destination");
		cheminErreur = prop.getProperty("chemin.erreur");
		JDBC_DRIVER_CLASS = prop.getProperty("JDBC_DRIVER_CLASS");
		JDBC_CONNECTION_URL = prop.getProperty("JDBC_CONNECTION_URL");
		option_cryptage=prop.getProperty("cryptage");
		
		
		//traitement certif
		File certificat = new File(cheminCertificat);
		if(certificat.isFile() && certificat.exists())
		{	
			//creation cartafika
			SecretKey key =  readKey(certificat);
			encrypter = new DesEncrypter(key);
			
		File root = new File(cheminSource);		
		if (root.isDirectory() && root.exists()) {
			// creation du livrable au niveau de la BD
			int idlivrable = verifierExistanceDulivrable(root.getName());
			if (idlivrable != 0) {
				System.out.println("Récuparation de ID livrable");
			} else {
				System.out.println("Insertion du livrable");
				insertLivrable("2", root.getName());
				idlivrable = verifierExistanceDulivrable(root.getName());
			}
			System.out.println("Livrable : " + idlivrable);
			// chargement des dossier un par un
			File[] dossiers = root.listFiles();
			int idLastVue = 0;
			//recuperation de id laste vue
			idLastVue=getFirstIdOfPiece();			
			int countDossier = 0;
			int totale = dossiers.length;
			for (File dossier : dossiers) {
				
				countDossier++;
				System.out.println("Traitement :" + countDossier+"/"+ totale);
				System.out.println("dossier name : " + dossier.getName());
				
				if (dossier.isDirectory() && root.exists() && verifierNameDossier(dossier.getName(),dossier.getAbsolutePath())) 
					{
						if (verifierCarton(dossier.getAbsolutePath()))
						{		
						String[] indexes_name = dossier.getName().split("_");
						String user_scan = indexes_name[0];
						String namedossier = indexes_name[1]+"_"+indexes_name[2];
						String date_scan = indexes_name[3];
						System.out.println("Verification de l'existance du dossier : " + namedossier);
						int idDossier = verifierExistanceDuDossier(namedossier);
						if (idDossier != 0) {
							System.out.println("Dossier existant : ID dossier : " + idDossier);
							// verification de existance des pieces au niveau de
							// la BD
							System.out.println("Verification de existance des pieces au niveau de la BD");
							if(verifierExistanceDespiecesSurunDossierExistant(idDossier))
							{
								//deplacement du TF sur le dossier injected
								System.out.println("Dossier injected : deplacement du TF "+namedossier+" sur le dossier injected");
								deplacerCartonErreur(dossier.getAbsolutePath(), cheminErreur+ "\\injected");
							}
							else
							{	
								System.out.println("Dossier injected Mais sans aucune piece injectées");
								System.out.println("Insertion des pieces");
								HashMap<String, String> pathAndBarcode = new HashMap<String, String>(); 
								pathAndBarcode.putAll(getPathAndBarCode(dossier.getAbsolutePath()));
								//insersion des pieces au niveau de la BD
								insertPieces(idDossier, pathAndBarcode,namedossier);
								//recuperation de la liste des ID avec bar code
								HashMap<String, String> idAndBarcode=getIDandBarcodeofTF(idDossier);
								//update des pieces avec l'utilisation du bare code pour la generation de id ordre								
								//verifier si la piece est la seconde au niveau de la BD
								Boolean updateFolder = true;
								for (int i = 0; i < pathAndBarcode.size(); i++) {
								        String id = Integer.toString(i);										
										String[] indexes = pathAndBarcode.get(id).toString().split(";");										
										//System.out.println("Code a bare : "+indexes[0]+ " :  id : " + idAndBarcode.get(indexes[0]));
										if(!setodre(idAndBarcode.get(indexes[0]),idLastVue))
										{
											//erreur mise a jour
											updateFolder=false;
											break;
										}
										else
										{
											File imageSource = new File(indexes[1]);
											File imageDestination=new File(destination+"\\"+namedossier+"\\"+indexes[0]+".tif");
											FileUtils.copyFile(imageSource, imageDestination);
										}
										idLastVue=Integer.parseInt(idAndBarcode.get(indexes[0]));
								}
								if(updateFolder)
								{
									//log mise a jour des pieces reussite
									System.out.println("mise a jour des pieces reussite");
									System.out.println("commencement d'archivage source ");
									if(deplacerCartonErreur(dossier.getAbsolutePath(), cheminArchiveSource))
									{
										System.out.println("Archivage source reussie");
										System.out.println("commencement d'archivage destination ");
										String cheminCartonDestination = destination+"\\"+namedossier+"\\";
										if(CopieCarton(cheminCartonDestination, cheminArchiveDestination))
										{
											System.out.println("Archivage destination reussie");
										}
										else
										{
											System.out.println("Erreur : Archivage destination");
										}
									}
									else
									{
										System.out.println("Erreur : Archivage source ");
									}
								}
								else
								{
									//erreur mise a jour des pieces 
									System.out.println("erreur mise a jour des pieces ");
									System.out.println("pas d'archivage");
								}
							}							
						}
						else 
						{
							System.out.println("Creation du dossier sur la BD");
							HashMap<String, String> pathAndBarcode = getPathAndBarCode(dossier.getAbsolutePath());
							String chemindossier = destination+"/"+namedossier+"/";
							if(insertDossier(idlivrable, pathAndBarcode.size(),namedossier, chemindossier, 2,user_scan,date_scan))
							{
							idDossier = verifierExistanceDuDossier(namedossier);
							System.out.println("ID dossier : " + idDossier);
							insertPieces(idDossier, pathAndBarcode,namedossier);
							//recuperation de la liste des ID avec bar code
							HashMap<String, String> idAndBarcode=getIDandBarcodeofTF(idDossier);
							Boolean updateFolder = true;
							for (int i = 0; i < pathAndBarcode.size(); i++) {
								String id = Integer.toString(i);
								String[] indexes = pathAndBarcode.get(id).toString().split(";");										
								//System.out.println("Code a bare : "+indexes[0]+ " :  id : " + idAndBarcode.get(indexes[0]));
								if(!setodre(idAndBarcode.get(indexes[0]),idLastVue))
								{
									//erreur mise a jour
									updateFolder=false;
									break;
								}
								else
								{
									File imageSource = new File(indexes[1]);
									File imageDestination=new File(destination+"\\"+namedossier+"\\"+indexes[0]+".tif");
									FileUtils.copyFile(imageSource, imageDestination);
								}
								idLastVue=Integer.parseInt(idAndBarcode.get(indexes[0]));
							}
							if(updateFolder)
							{
								//log mise a jour des pieces reussite
								System.out.println("mise a jour des pieces reussite");
								System.out.println("commencement d'archivage source ");
								if(deplacerCartonErreur(dossier.getAbsolutePath(), cheminArchiveSource))
								{
									System.out.println("Archivage source reussie");
									System.out.println("commencement d'archivage destination ");
									String cheminCartonDestination = destination+"\\"+namedossier+"\\";
									if(CopieCarton(cheminCartonDestination, cheminArchiveDestination))
									{
										System.out.println("Archivage destination reussie");
									}
									else
									{
										System.out.println("Erreur : Archivage destination");
									}
								}
								else
								{
									System.out.println("Erreur : Archivage source ");
								}
							}
							else
							{
								//erreur mise a jour des pieces 
								System.out.println("erreur mise a jour des pieces ");
								System.out.println("pas d'archivage");
							}
						}
						else
						{
							System.out.println("Erreur insertion dossier");
						}
					}
				}
			}
			// fin insertion des pieces
			System.out.println("Fin ");
			}
			}		
		else
		{
			System.out.println("Erreur : attention le chemin source spicifié n'est pas correct");
		}
		}
		else
		{
			System.out.println("Erreur ! Certificat introuvable");
		}
	}
	
	public static boolean verifierNameDossier(String nameDossier,String cartonPath)
	{
		String[] indexes = nameDossier.split("_");
		if(indexes.length==4)
		{
			return true;
		}
		else
		{
			System.out.println("Erreur nomination !!");
			System.out.println("Déplacement de dossier : " + nameDossier);
			deplacerCartonErreur(cartonPath, cheminErreur
					+ "\\ErreurNomination");
			return false;
		}
		
	}
	
	public static int getFirstIdOfPiece()
	{
		int idFirstVue = 0;
		String requette = "SELECT ISNULL(MAX(id_vue), 0) as max FROM dbo.TB_Vues";		
		//System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String valPath = rs.getString("max");
				if(valPath==null)
				{
					idFirstVue=0;
				}
				else
				{
				idFirstVue = Integer.parseInt(valPath);
				}
				//System.out.println(valPath);
			}
		} catch (ClassNotFoundException e) {
			System.err.println(e);
			return 0;
		} catch (SQLException e) {
			System.err.println(e);
			return 0;
		}
		
		return idFirstVue;
	}
	
	public static Boolean setodre(String idVue,int valueOrdre)
	{
		Boolean condition = true;
		String requette ="UPDATE [dbo].[TB_Vues] SET [id_status]=1,[numero_ordre]="+valueOrdre+" where (id_vue="+idVue+")";		
		//System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_imp = connection
					.prepareStatement(requette);
			int st_imp = prepStmt_imp.executeUpdate();
			if (st_imp > 0) {
				connection.commit();
				connection.setAutoCommit(false);
				//System.out.println("upadated");
			} else {
				//System.out.println("Erreur upadated");
				condition = false;
			}
		} catch (ClassNotFoundException e) {
			condition = false;
			System.err.println(e);
		} catch (SQLException e) {
			condition = false;
			System.err.println(e);
		}		
		return condition;
	}
	
	public static HashMap<String, String> getIDandBarcodeofTF(int idTF)
	{
		HashMap<String, String> idAndBarcode = new HashMap<String, String>();	 
		String requette = "SELECT [id_vue] ,[bar_code] as num FROM [dbo].[TB_Vues] where id_dossier="+ idTF + "";
		
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String id_vue_string = rs.getString("id_vue");	
				String bar_code_string = rs.getString(2);				
				idAndBarcode.put(bar_code_string.trim(), id_vue_string.trim());
			}
		} catch (ClassNotFoundException e) {
			System.err.println(e);
			return null;
		} catch (SQLException e) {
			System.err.println(e);
			return null;
		}	
		
		return idAndBarcode;
	}

	public static Boolean verifierExistanceDespiecesSurunDossierExistant(
			int idDossier) {
		Boolean condition = false;

		String requette = "SELECT COUNT(*)as num FROM [dbo].[TB_Vues] where id_dossier="
				+ idDossier + "";
		//System.out.println(requette);

		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String valPath = rs.getString("num");
				int nbrBds = Integer.parseInt(valPath);
				if (nbrBds != 0) {
					condition = true;
					//System.out.println("les pieces du dossier existe");
				} else {
					condition = false;
					//System.out.println("les pieces du Dossier non existe existe");
				}

			}
		} catch (ClassNotFoundException e) {
			System.err.println(e);
			return false;
		} catch (SQLException e) {
			System.err.println(e);
			return false;
		}

		return condition;

	}

	public static int verifierExistanceDuDossier(String nameDossier) {
		int idDossier = 0;
		String requette = "SELECT [id_dossier] FROM [dbo].[TB_Dossier] where name_dossier='"
				+ nameDossier + "'";
		//System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String valPath = rs.getString("id_dossier");
				idDossier = Integer.parseInt(valPath);
				// System.out.println(valPath);
			}
		} catch (ClassNotFoundException e) {
			System.err.println(e);
			return 0;
		} catch (SQLException e) {
			System.err.println(e);
			return 0;
		}

		return idDossier;
	}

	public static int verifierExistanceDulivrable(String nameLivrable)
			throws ClassNotFoundException {
		int idLivrable = 0;
		String requette = "SELECT [id_livrable] FROM [dbo].[TB_Livrable] where nom_livrable='"
				+ nameLivrable + "'";
		//System.out.println(requette);

		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String valPath = rs.getString("id_livrable");
				idLivrable = Integer.parseInt(valPath);
				// System.out.println(valPath);
			}
		} catch (ClassNotFoundException e) {
			System.err.println(e);
			return 0;
		} catch (SQLException e) {
			System.err.println(e);
			return 0;
		}

		return idLivrable;
	}

	public static Boolean insertDossier(int iDlivrable, int nbrImages,
			String nameDossier, String cheminDossier, int userIndex,String user_scan,String date_scan ) {
		
		if(option_cryptage.equalsIgnoreCase("oui"))
		{
		String encryptedChemin1 = encrypter.encrypt(cheminDossier);
		String encryptedChemin = encryptedChemin1.replaceAll("\\s","");
		cheminDossier=encryptedChemin;
		}
		
		Boolean condition = true;
		String requette = "INSERT INTO [dbo].[TB_Dossier] ([id_status],[id_livrable],[name_dossier],[url],[nb_image],[date_inject],[user_inject],[date_scan],[user_scan]) VALUES (0,"+iDlivrable+",'"
				+ nameDossier
				+ "','"
				+ cheminDossier
				+ "',"
				+ nbrImages
				+ ",GETDATE()," 
				+ userIndex 
				+ ",'"
				+ date_scan 
				+ "','"
				+ user_scan
				+"')";
		//System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);			
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_imp = connection
					.prepareStatement(requette);
			int st_imp = prepStmt_imp.executeUpdate();
			if (st_imp > 0) {
				connection.commit();
				connection.setAutoCommit(false);
				//System.out.println("insered");
			} else {
				//System.out.println("Erreur insered");
				condition = false;
			}
		} catch (ClassNotFoundException e) {
			condition = false;
			System.err.println(e);
		} catch (SQLException e) {
			condition = false;
			System.err.println(e);
		}

		return condition;
	}

	public static Boolean insertLivrable(String iduser, String nameLivrable)
	{
		Boolean condition = true;
		String requette = "INSERT INTO [dbo].[TB_Livrable] ([date_livrable],[user_livrable],[nom_livrable],[etat]) VALUES (GETDATE(),"
				+ iduser + ",'" + nameLivrable + "',0)";
		//System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			//System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_imp = connection
					.prepareStatement(requette);
			int st_imp = prepStmt_imp.executeUpdate();
			if (st_imp > 0) {
				connection.commit();
				connection.setAutoCommit(false);
				//System.out.println("insered");
			} else {
				//System.out.println("Erreur insered");
				condition = false;
			}
		} catch (ClassNotFoundException e) {
			condition = false;
			System.err.println(e);
		} catch (SQLException e) {
			condition = false;
			System.err.println(e);
		}

		return condition;
	}
	
	public static HashMap<String, String> getPathAndBarCode(String cartonPath)
			throws IOException {
		HashMap<String, String> dictioPathAndBarcode = new HashMap<String, String>();
		String pathDocOrder = cartonPath + "\\Doc_Order.TXT";
		ArrayList<String> listePieceInDossier = new ArrayList<String>();
		File docOrder = new File(pathDocOrder);
		
		// chargement de la liste des pieces sur arrayliste
		String line;
		BufferedReader br = new BufferedReader(new FileReader(docOrder));
		while ((line = br.readLine()) != null) {
			if (!line.equalsIgnoreCase("")) {
				listePieceInDossier.add(line);
			}
		}
		br.close();
		// fin chargement de la liste des pieces sur arrayliste

		for (int i = 0; i < listePieceInDossier.size(); i++) {
			String path1pg = cartonPath + "\\" + listePieceInDossier.get(i)	+ "\\1.pg";
			String pathCommands = cartonPath + "\\"	+ listePieceInDossier.get(i) + "\\COMMANDS";
			String barcode = listePieceInDossier.get(i);
			//String barcode = getBarCode(pathCommands);
			dictioPathAndBarcode.put(Integer.toString(i), barcode+";"+path1pg);
		}

		return dictioPathAndBarcode;
	}
	
	public static Boolean insertPieces(int idDossier,HashMap<String, String> pathAndBarcode,String namedossier) throws SQLException {
		Boolean condition = true;
		// creation d'une requete qui insert la totalite des pieces d'un seul
		// coup
		String requette = "";
		int count=0;
		for (int i = 0; i < pathAndBarcode.size(); i++) {
			count++;
			String id = Integer.toString(i);
			//System.out.println(pathAndBarcode.get(id).toString());
			String[] indexes = pathAndBarcode.get(id).toString().split(";");	
			String chemin = destination+"\\"+namedossier+"\\"+indexes[0]+".tif";
//			if(option_cryptage.equalsIgnoreCase("oui"))
//			{
//			String encryptedChemin1 = encrypter.encrypt(chemin);
//			String encryptedChemin = encryptedChemin1.replaceAll("\\s","");
//			if (i == 0) {
//				requette = requette + "('" + indexes[0] + "'," + idDossier + ",'"+ encryptedChemin + "')";
//			} else {
//				requette = requette + ",('" + indexes[0] + "'," + idDossier + ",'"+ encryptedChemin + "')";
//			}
//			}
//			else
//			{
//			if (i == 0) {
//				requette = requette + "('" + indexes[0] + "'," + idDossier + ",'"+ chemin + "')";
//			} else {
//				requette = requette + ",('" + indexes[0] + "'," + idDossier + ",'"+ chemin + "')";
//				}
//			}
			requette = requette + "('" + indexes[0] + "'," + idDossier + ",'"+ chemin + "')";
				String requettefinal = "INSERT INTO [dbo].[TB_Vues] ([bar_code],[id_dossier],[url]) VALUES "+ requette + " ;";
				System.out.println(requettefinal);
				try {
					Class.forName(JDBC_DRIVER_CLASS);
					connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
					//System.out.println("OPEN CONNEXION DATABASE OK");
					PreparedStatement prepStmt_imp = connection
							.prepareStatement(requettefinal);
					int st_imp = prepStmt_imp.executeUpdate();
					if (st_imp > 0) {
						connection.commit();
						connection.setAutoCommit(false);
						System.out.println("insered");
					} else {
						System.out.println("Erreur insered");
						condition = false;
					}
				} catch (ClassNotFoundException e) {
					condition = false;
					System.err.println(e);
				} catch (SQLException e) {
					condition = false;
					System.err.println(e);
				}
				
			
			requette="";
		}
		return condition;
	}

	public static Boolean verifierCarton(String cartonPath) throws IOException {
		Boolean condition = true;
		File carton = new File(cartonPath);
		File[] files = carton.listFiles();
		Boolean fileDoc_Order = false;
		String cheminDocOrder = "";
		for (File fichier : files) {
			if (fichier.isFile()) {
				if (fichier.getName().equalsIgnoreCase("Doc_Order.TXT")) {
					fileDoc_Order = true;
					cheminDocOrder = fichier.getAbsolutePath();
				}
			}
		}
		if (fileDoc_Order) {
			if (verifierDocOrder(cheminDocOrder)) {
				if (verifierExistanceDesPieces(cheminDocOrder)) {
					// dossier OK
				} else {
					deplacerCartonErreur(cartonPath, cheminErreur
							+ "\\PieceNonExistant");
					condition = false;
				}
			} else {
				deplacerCartonErreur(cartonPath, cheminErreur
						+ "\\DossierFileNonExistant");
				condition = false;
			}
		} else {
			condition = false;
			deplacerCartonErreur(cartonPath, cheminErreur + "\\AbsenceDocOrder");
		}
		return condition;
	}

	public static Properties load(String filename) throws IOException,
			FileNotFoundException {
		Properties properties = new Properties();
		FileInputStream input = new FileInputStream(filename);
		try {
			properties.load(input);
		} catch (IOException w) {
			System.out.println(w.getMessage());
			System.exit(-1);
		} finally {
			input.close();
		}
		return properties;
	}

	public static Boolean deplacerCartonErreur(String cartonPath,
			String cheminFolderErreurCarton) {
		Boolean condition = true;
		File carton = new File(cartonPath);
		File folderErreur = new File(cheminFolderErreurCarton);
		if (!folderErreur.exists()) {
			folderErreur.mkdir();
		}
		File folderErreurCarton = new File(folderErreur.getAbsolutePath()
				+ "\\" + carton.getName());
		try {
			FileUtils.moveDirectory(carton, folderErreurCarton);
		} catch (IOException e) {
			// TODO Auto-generated catch block*
			condition = false;
			e.printStackTrace();
		}
		return condition;
	}
	
	public static Boolean CopieCarton(String cartonPath,
			String cheminFolderErreurCarton) {
		Boolean condition = true;
		File carton = new File(cartonPath);
		File folderErreur = new File(cheminFolderErreurCarton);
		if (!folderErreur.exists()) {
			folderErreur.mkdir();
		}
		File folderErreurCarton = new File(folderErreur.getAbsolutePath()
				+ "\\" + carton.getName());
		try {
			FileUtils.copyDirectory(carton, folderErreurCarton);
		} catch (IOException e) {
			// TODO Auto-generated catch block*
			condition = false;
			e.printStackTrace();
		}
		return condition;
	}

	public static boolean verifierDocOrder(String pathDocOrder)
			throws IOException {
		Boolean condition = true;
		ArrayList<String> listePieceInDossier = new ArrayList<String>();
		File docOrder = new File(pathDocOrder);

		// chargement de la liste des pieces sur arrayliste
		String line;
		BufferedReader br = new BufferedReader(new FileReader(docOrder));
		while ((line = br.readLine()) != null) {
			if (!line.equalsIgnoreCase("")) {
				listePieceInDossier.add(line);
			}
		}
		br.close();
		// fin chargement de la liste des pieces sur arrayliste
		if (listePieceInDossier.size() > 0) {

			for (int i = 0; i < listePieceInDossier.size(); i++) {
				File piece = new File(docOrder.getParent() + "\\"
						+ listePieceInDossier.get(i));
				if (!piece.exists()) {
					condition = false;
					break;
				}
			}
		} else {
			condition = false;
		}

		return condition;
	}

	public static boolean verifierExistanceDesPieces(String pathDocOrder)
			throws IOException {
		Boolean condition = true;
		ArrayList<String> listePieceInDossier = new ArrayList<String>();
		File docOrder = new File(pathDocOrder);

		// chargement de la liste des pieces sur arrayliste
		String line;
		BufferedReader br = new BufferedReader(new FileReader(docOrder));
		while ((line = br.readLine()) != null) {
			if (!line.equalsIgnoreCase("")) {
				listePieceInDossier.add(line);
			}
		}
		br.close();
		// fin chargement de la liste des pieces sur arrayliste

		for (int i = 0; i < listePieceInDossier.size(); i++) {
			File piece = new File(docOrder.getParent() + "\\"
					+ listePieceInDossier.get(i) + "\\1.pg");
			if (!piece.exists()) {
				condition = false;
				break;
			}
		}
		return condition;
	}
	
	public static String getBarCode(String pathCommand) throws IOException {
		String barcode = "";

		File toread = new File(pathCommand);
		// lecture du fichier commmand
		BufferedReader monFich = new BufferedReader(new FileReader(toread));
		String ligne = "";
		while ((ligne = monFich.readLine()) != null) {
			if (!ligne.equalsIgnoreCase("")) {
				StringTokenizer st = new StringTokenizer(ligne);
				String name = st.nextToken();
				if (name.equalsIgnoreCase("BARCODE")) {					
					barcode = ligne.substring(name.length()).trim();
					
//					String namePiece = ligne.substring(name.length()).trim();
//					String[] indexes = namePiece.split("_");
//					barcode = indexes[2];
				}
			}
		}// fin
		monFich.close();
		return barcode;

	}
	
	/** Read a TripleDES secret key from the specified file */
    public static SecretKey readKey(File f) throws IOException,
            NoSuchAlgorithmException, InvalidKeyException,
            InvalidKeySpecException {
        // Read the raw bytes from the keyfile
        DataInputStream in = new DataInputStream(new FileInputStream(f));
        byte[] rawkey = new byte[(int) f.length()];
        in.readFully(rawkey);
        in.close();

        // Convert the raw bytes to a secret key like this
        DESKeySpec keyspec = new DESKeySpec(rawkey);
        SecretKeyFactory keyfactory = SecretKeyFactory.getInstance("DES");
        SecretKey key = keyfactory.generateSecret(keyspec);
        return key;
    }
}
