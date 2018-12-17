CREATE VIEW vwCustomer AS 
	SELECT 
	c.Id,
	c.GenderId,
	c.CityId,
	c.RegionId,
	c.ClassificationId,
	c.UserId,
	c.Name, 
	c.Phone,
	g.Name AS Gender,
	ci.Name AS City,
	rci.Name AS RegionCity,
	r.Name AS Region,
	c.LastPurchase,
	cl.Name AS Classification, 
	us.Login,
	us.Email,
	us.Password,
	ur.Name AS UserRole,
	ur.IsAdmin
FROM Customer c
LEFT JOIN City ci ON  c.CityId = ci.Id
LEFT JOIN Region rci ON ci.RegionId = rci.id
LEFT JOIN Classification cl ON c.ClassificationId = cl.Id
LEFT JOIN Gender g ON c.GenderId = g.Id
LEFT JOIN Region r ON c.RegionId = r.id
LEFT JOIN UserSys us ON c.UserId = us.Id
LEFT JOIN UserRole ur ON us.UserRoleId = ur.Id
