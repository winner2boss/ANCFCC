package injection_NV_Abbyy;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;

import org.apache.commons.io.FilenameUtils;
import org.apache.commons.io.FileUtils;

public class Main {

	public static String cheminErreur;
	public static String cheminArchiveSource;
	public static String cheminArchiveDestination;
	public static String destination;
	public static String JDBC_DRIVER_CLASS;
	public static String JDBC_CONNECTION_URL;
	public static Connection connection = null;
	public static HashMap<String, String> pathAndBarcode = new HashMap<String, String>();

	public static void main(String[] args) throws FileNotFoundException,
			IOException, ClassNotFoundException, SQLException {
		// TODO Auto-generated method stub
		Properties prop = load("config.properties");
		String cheminSource = prop.getProperty("chemin.source");
		String cheminCertificat = prop.getProperty("chemin.certificat");
		destination = prop.getProperty("chemin.destination");
		cheminArchiveSource = prop.getProperty("chemin.archive.source");
		cheminArchiveDestination = prop
				.getProperty("chemin.archive.destination");
		cheminErreur = prop.getProperty("chemin.erreur");
		JDBC_DRIVER_CLASS = prop.getProperty("JDBC_DRIVER_CLASS");
		JDBC_CONNECTION_URL = prop.getProperty("JDBC_CONNECTION_URL");

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
			// recuperation de id laste vue
			idLastVue = getFirstIdOfPiece();
			int countDossier = 0;
			int totale = dossiers.length;
			for (File dossier : dossiers) {
				pathAndBarcode.clear();
				countDossier++;
				System.out
						.println("Traitement :" + countDossier + "/" + totale);
				System.out.println("dossier name : " + dossier.getName());
				if (dossier.isDirectory() && root.exists()
						&& verifierNameDossier(dossier.getName(),
								dossier.getAbsolutePath())) {
					if (verifierCarton(dossier.getAbsolutePath(),
							dossier.getName())) {
						System.out.println(dossier.getName()
								+ " dossier valide");
						String[] indexes_name = dossier.getName().split("_");
						String user_scan = indexes_name[2];
						String namedossier = indexes_name[0] + "_"
								+ indexes_name[1];
						String date_scan = indexes_name[3];
						System.out
								.println("Verification de l'existance du dossier : "
										+ namedossier);
						int idDossier = verifierExistanceDuDossier(namedossier);
						//si le n'existe pas injecté
						if (idDossier == 0)	
						{
							System.out.println("Creation du dossier sur la BD");
							ArrayList<Integer> listebyordre = getordrepieces(dossier.getAbsolutePath());
							String chemindossier = destination+"/"+namedossier+"/";							
							if(insertDossier(idlivrable, listebyordre.size(),namedossier, chemindossier, 2,user_scan,date_scan))
							{
								idDossier = verifierExistanceDuDossier(namedossier);
								System.out.println("ID dossier : " + idDossier);
								insertPieces(idDossier,listebyordre,namedossier,dossier.getAbsolutePath());
								//recuperation de la liste des ID avec bar code
								HashMap<String, String> idAndBarcode=getIDandBarcodeofTF(idDossier);
								
								Boolean updateFolder = true;
								for (int i = 0; i < listebyordre.size(); i++) {
									String ordre = listebyordre.get(i).toString();
									String md5 = pathAndBarcode.get(ordre);
									String idrecupered = idAndBarcode.get(md5);
									if(!setodre(idrecupered,idLastVue))
									{
										//erreur mise a jour
										updateFolder=false;
										break;
									}
									else
									{
										File imageSource = new File(cheminSource+"\\"+dossier.getName()+"\\Page "+listebyordre.get(i)+".tif");
										File imageDestination=new File(destination+namedossier+"\\"+md5+".tif");
										FileUtils.copyFile(imageSource, imageDestination);
									}
									idLastVue=Integer.parseInt(idrecupered);
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
						else
						{
							System.out.println("Dossier existant : ID dossier : " + idDossier);
							// verification de existance des pieces au niveau de
							// la BD
							System.out.println("Verification de existance des pieces au niveau de la BD");
							if (verifierExistanceDespiecesSurunDossierExistant(idDossier)) {
								// deplacement du TF sur le dossier injected
								System.out.println("Dossier injected : deplacement du TF "+ namedossier+ " sur le dossier injected");
								deplacerCartonErreur(dossier.getAbsolutePath(),cheminErreur + "\\injected");
							} else {
								System.out.println("Dossier injected Mais sans aucune piece insérés");
								System.out.println("Insertion des pieces");
								ArrayList<Integer> listebyordre = getordrepieces(dossier.getAbsolutePath());
								insertPieces(idDossier,listebyordre,namedossier,dossier.getAbsolutePath());
								//recuperation de la liste des ID avec bar code
								HashMap<String, String> idAndBarcode=getIDandBarcodeofTF(idDossier);								
								Boolean updateFolder = true;
								for (int i = 0; i < listebyordre.size(); i++) {
									String ordre = listebyordre.get(i).toString();
									String md5 = pathAndBarcode.get(ordre);
									String idrecupered = idAndBarcode.get(md5);
									if(!setodre(idrecupered,idLastVue))
									{
										//erreur mise a jour
										updateFolder=false;
										break;
									}
									else
									{
										File imageSource = new File(cheminSource+"\\"+dossier.getName()+"\\Page "+listebyordre.get(i)+".tif");
										File imageDestination=new File(destination+namedossier+"\\"+md5+".tif");
										FileUtils.copyFile(imageSource, imageDestination);
									}
									idLastVue=Integer.parseInt(idrecupered);
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
					} else {
						System.out.println(dossier.getName()
								+ " dossier invalide");
					}
				}
			}
		}
	}	
	
	public static Boolean insertDossier(int iDlivrable, int nbrImages,
			String nameDossier, String cheminDossier, int userIndex,String user_scan,String date_scan ) {
		
		
		
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
	
	public static Boolean insertPieces(int idDossier,ArrayList<Integer> listebyordre,String namedossier,String cartonPath) throws SQLException, IOException {
		Boolean condition = true;
		HashMap<Integer, String> dictioOrdreAndPath = new HashMap<Integer, String>();
		File carton = new File(cartonPath);
		File[] files = carton.listFiles();		
		for (File tiff : files) 
		{			
			String extension = "";
			int i = tiff.getName().lastIndexOf('.');
			if (i > 0) {
				extension = tiff.getName().substring(i + 1);
			}
			if (extension.equalsIgnoreCase("tif")) {
				String fileNameWithOutExt = FilenameUtils.removeExtension(tiff.getName());
				String[] indexes = fileNameWithOutExt.split(" ");
				dictioOrdreAndPath.put(Integer.parseInt(indexes[1]), tiff.getAbsolutePath());				
			}
		}
		String requette = "";
		for (int i = 0; i < listebyordre.size(); i++) 
		{
			FileInputStream fis = new FileInputStream(dictioOrdreAndPath.get(listebyordre.get(i)));
			String barcode = org.apache.commons.codec.digest.DigestUtils.md5Hex(fis);
			fis.close();
			String chemin = destination+"\\"+namedossier+"\\"+barcode+".tif";						
			if (i == 0) {
				requette = requette + "('" + barcode + "'," + idDossier + ",'"+ chemin + "')";
			} else {
				requette = requette + ",('" + barcode + "'," + idDossier + ",'"+ chemin + "')";
				}
			pathAndBarcode.put(listebyordre.get(i).toString(), barcode);
		}
		String requettefinal = "INSERT INTO [dbo].[TB_Vues] ([bar_code],[id_dossier],[url]) VALUES "+ requette + " ;";
		//System.out.println(requettefinal);
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
		return condition;
	}
	
	public static ArrayList<Integer>  getordrepieces(String cartonPath)
			throws IOException {				
		ArrayList<Integer> liste = new ArrayList<Integer>();
		File carton = new File(cartonPath);
		File[] files = carton.listFiles();		
		for (File tiff : files) 
			{			
			String extension = "";
			int i = tiff.getName().lastIndexOf('.');
			if (i > 0) {
				extension = tiff.getName().substring(i + 1);
			}
			if (extension.equalsIgnoreCase("tif")) {
				String fileNameWithOutExt = FilenameUtils.removeExtension(tiff.getName());
				String[] indexes = fileNameWithOutExt.split(" ");
				
				liste.add(Integer.parseInt(indexes[1]));
			}
			}		
			Collections.sort(liste);
//			for (int i = 0; i < liste.size(); i++) {
//				System.out.println(dictioOrdreAndPath.get(liste.get(i)));
//				FileInputStream fis = new FileInputStream(dictioOrdreAndPath.get(liste.get(i)));
//				String md5 = org.apache.commons.codec.digest.DigestUtils.md5Hex(fis);
//				System.out.println("Image :" + dictioOrdreAndPath.get(liste.get(i)) + " Md5 : " + md5);
//				dictioPathAndBarcode.put(md5, dictioOrdreAndPath.get(liste.get(i)));
//			}
		return liste;
	}

	
    public static Boolean verifierExistanceDespiecesSurunDossierExistant(
			int idDossier) {
		Boolean condition = false;

		String requette = "SELECT COUNT(*)as num FROM [dbo].[TB_Vues] where id_dossier="
				+ idDossier + "";
		// System.out.println(requette);

		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			// System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String valPath = rs.getString("num");
				int nbrBds = Integer.parseInt(valPath);
				if (nbrBds != 0) {
					condition = true;
					// System.out.println("les pieces du dossier existe");
				} else {
					condition = false;
					// System.out.println("les pieces du Dossier non existe existe");
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
		// System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			// System.out.println("OPEN CONNEXION DATABASE OK");
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

	public static Boolean verifierCarton(String cartonPath, String nameDossier)
			throws IOException {
		Boolean condition = true;

		File carton = new File(cartonPath);
		File[] files = carton.listFiles();
		if (files.length > 0) {
			for (File tiff : files) {
				if (tiff.isFile()) {
					String nametiff = tiff.getName();

					String extension = "";

					int i = nametiff.lastIndexOf('.');
					if (i > 0) {
						extension = nametiff.substring(i + 1);
					}
					if (extension.equalsIgnoreCase("tif")) {
						String[] indexes = nametiff.split(" ");
						if (indexes[0].equalsIgnoreCase("Page")) {
							char[] c_arr = indexes[1].toCharArray();
							if (c_arr.length == 0) {
								System.out
										.println("Erreur nomination piece !!");
								System.out.println("Déplacement de dossier : "
										+ nameDossier);
								deplacerCartonErreur(cartonPath, cheminErreur
										+ "\\ErreurnominationPieces");
								condition = false;
								break; // if you press cancel it will exit
							}
							try {
								String numpage = String.valueOf(c_arr[0]);
								int num = Integer.parseInt(numpage);
								// System.out.println(nametiff);
								condition = true;
							} catch (NumberFormatException ex) {
								System.out
										.println("Erreur nomination piece !!");
								System.out.println("Déplacement de dossier : "
										+ nameDossier);
								deplacerCartonErreur(cartonPath, cheminErreur
										+ "\\ErreurnominationPieces");
								condition = false;
								break;
							}

						} else {
							System.out.println("Erreur nomination piece !!");
							System.out.println("Déplacement de dossier : "
									+ nameDossier);
							deplacerCartonErreur(cartonPath, cheminErreur
									+ "\\ErreurnominationPieces");
							return false;
						}
					} else {
						if (!extension.equalsIgnoreCase("db")) {
							System.out.println(extension);
							System.out.println("Erreur extention piece !!");
							System.out.println("Déplacement de dossier : "
									+ nameDossier);
							deplacerCartonErreur(cartonPath, cheminErreur
									+ "\\ErreurExtentionPieces");
							return false;
						}

					}
				}
			}
		} else {
			System.out.println("Dossier Vide !!");
			System.out.println("Déplacement de dossier : " + nameDossier);
			deplacerCartonErreur(cartonPath, cheminErreur + "\\CartonVide");
			return false;
		}
		return condition;
	}

	public static boolean verifierNameDossier(String nameDossier,
			String cartonPath) {
		String[] indexes = nameDossier.split("_");
		if (indexes.length == 4) {
			return true;
		} else {
			System.out.println("Erreur nomination !!");
			System.out.println("Déplacement de dossier : " + nameDossier);
			deplacerCartonErreur(cartonPath, cheminErreur
					+ "\\ErreurNomination");
			return false;
		}

	}

	public static Boolean deplacerCartonErreur(String cartonPath,
			String cheminFolderErreurCarton) {
		Boolean condition = true;
		File carton = new File(cartonPath);
		File folderErreur = new File(cheminFolderErreurCarton);
		if (!folderErreur.exists()) {
			folderErreur.mkdir();
		}
		File folderErreurCarton = new File(folderErreur.getAbsolutePath()+ "\\" + carton.getName());
		try {
			FileUtils.moveDirectory(carton, folderErreurCarton);			
		} catch (IOException e) {
			// TODO Auto-generated catch block*
			condition = false;
			e.printStackTrace();
		}
		return condition;
	}

	public static int getFirstIdOfPiece() {
		int idFirstVue = 0;
		String requette = "SELECT ISNULL(MAX(id_vue), 0) as max FROM dbo.TB_Vues";
		// System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			// System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_exp = connection
					.prepareStatement(requette);
			ResultSet rs = prepStmt_exp.executeQuery();
			while (rs.next()) {
				String valPath = rs.getString("max");
				if (valPath == null) {
					idFirstVue = 0;
				} else {
					idFirstVue = Integer.parseInt(valPath);
				}
				// System.out.println(valPath);
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

	public static Boolean insertLivrable(String iduser, String nameLivrable) {
		Boolean condition = true;
		String requette = "INSERT INTO [dbo].[TB_Livrable] ([date_livrable],[user_livrable],[nom_livrable],[etat]) VALUES (GETDATE(),"
				+ iduser + ",'" + nameLivrable + "',0)";
		// System.out.println(requette);
		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			// System.out.println("OPEN CONNEXION DATABASE OK");
			PreparedStatement prepStmt_imp = connection
					.prepareStatement(requette);
			int st_imp = prepStmt_imp.executeUpdate();
			if (st_imp > 0) {
				connection.commit();
				connection.setAutoCommit(false);
				// System.out.println("insered");
			} else {
				// System.out.println("Erreur insered");
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

	public static int verifierExistanceDulivrable(String nameLivrable)
			throws ClassNotFoundException {
		int idLivrable = 0;
		String requette = "SELECT [id_livrable] FROM [dbo].[TB_Livrable] where nom_livrable='"
				+ nameLivrable + "'";
		// System.out.println(requette);

		try {
			Class.forName(JDBC_DRIVER_CLASS);
			connection = DriverManager.getConnection(JDBC_CONNECTION_URL);
			// System.out.println("OPEN CONNEXION DATABASE OK");
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

}
